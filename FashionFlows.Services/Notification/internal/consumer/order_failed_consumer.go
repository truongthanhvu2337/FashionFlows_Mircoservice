package consumer

import (
	"encoding/json"
	"log"
	"notificationservice/config"
	"notificationservice/internal/model"
	"notificationservice/internal/model/events"
	"notificationservice/internal/repository"
	"time"

	"github.com/streadway/amqp"
)

type OrderFailedConsume struct {
	repo *repository.NotificationRepository
}

func ConsumeOrderFailedEvent(repo *repository.NotificationRepository) {
	conn, ch := config.ConnectRabbitMQ()
	defer conn.Close()
	defer ch.Close()

	queueName := "golang-order-failed-queue"
	q, err := ch.QueueDeclare(queueName, true, false, false, false, nil)
	if err != nil {
		log.Println(err)
	}

	err = ch.QueueBind(q.Name, "", "FashionFlows.BuildingBlock.Domain.Events:OrderFailedEvent", false, nil)
	if err != nil {
		log.Println(err)
	}

	msgs, err := ch.Consume(q.Name, "", false, false, false, false, nil)
	if err != nil {
		log.Println(err)
	}

	consumer := OrderFailedConsume{repo: repo}
	for msg := range msgs {
		consumer.handleOrderFailedEvent(msg)
	}
}

func (s *OrderFailedConsume) handleOrderFailedEvent(msg amqp.Delivery) {
	var event events.OrderFailedEvent
	
	err := json.Unmarshal(msg.Body, &event)
	if err != nil {
		log.Print(err)
		msg.Nack(false, true)
		return
	}
	notification := model.Notification {
		UserID: event.UserID,
		Message: "Order created failed",
		IsRead: false,
		CreatedAt: time.Now(),
	}
	error := s.repo.Create(&notification)
	if error != nil {
		log.Print(err)
	}

	msg.Ack(false)
}