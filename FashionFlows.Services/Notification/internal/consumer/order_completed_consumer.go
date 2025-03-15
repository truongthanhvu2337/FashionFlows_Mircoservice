package consumer

import (
	"encoding/json"
	"log"
	"notificationservice/internal/model"
	"notificationservice/internal/model/events"
	"notificationservice/internal/repository"
	"time"

	"github.com/streadway/amqp"
)

type OrderCompletedConsumer struct {
	repo *repository.NotificationRepository
}

func ConsumeCompletedEvent(ch *amqp.Channel, repo *repository.NotificationRepository) {
	queueName := "order-completed-queue"
	q, err := ch.QueueDeclare(queueName, true, false, false, false, nil)
	if err != nil {
		log.Println(err)
	}

	err = ch.QueueBind(q.Name, "", "FashionFlows.BuildingBlock.Domain.Events:OrderCompletedEvent", false, nil)
	if err == nil {
		log.Println("Queue successfully bound to exchange")
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
	var temp struct {
		Message json.RawMessage `json:"message"`
	}	

	err := json.Unmarshal(msg.Body, &temp)
	if err != nil {
		log.Printf("Failed to parse JSON: %v", err)
		msg.Nack(false, true)
		return
	}

	err = json.Unmarshal(temp.Message, &event)
	if err != nil {
		log.Printf("Failed to parse 'message' field: %v", err)
		msg.Nack(false, true)
		return
	}

	notification := model.Notification {
		UserID: event.UserID,
		Message: "Order creation completed please complete your payment",
		IsRead: false,
		CreatedAt: time.Now(),
	}

	error := r.repo.Create(&notification)
	if error != nil {
		log.Print(err)
	}

	msg.Ack(false)
}