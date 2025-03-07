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

type OrderCompletedConsumer struct {
	repo *repository.NotificationRepository
}

func ConsumeCompletedEvent(repo *repository.NotificationRepository) {
	conn, ch := config.ConnectRabbitMQ()
	defer conn.Close()
	defer ch.Close()

	queueName := "golang-order-failed-queue"
	q, err := ch.QueueDeclare(queueName, true, false, false, false, nil)
	if err != nil {
		log.Println(err)
	}

	err = ch.QueueBind(q.Name, "", "FashionFlows.BuildingBlock.Domain.Events:OrderCompletedEvent", false, nil)
	if err != nil {
		log.Println(err)
	}

	msgs, err := ch.Consume(q.Name, "", false, false, false, false, nil)
	if err != nil {
		log.Println(err)
	}

	consumer := OrderCompletedConsumer{repo: repo}
	for msg := range msgs {
		consumer.handleOrderCompletedEvent(msg)
	}
}

func (r* OrderCompletedConsumer) handleOrderCompletedEvent(msg amqp.Delivery){
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

	error := r.repo.Create(&notification)
	if error != nil {
		log.Print(err)
	}

	msg.Ack(false)
}