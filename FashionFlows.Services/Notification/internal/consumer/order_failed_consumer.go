package consumer

import (
	"encoding/json"
	"log"
	"notificationservice/config"
	"notificationservice/internal/model/events"

	"github.com/streadway/amqp"
)

func handleOrderFailedEvent(msg amqp.Delivery) {
	var event events.OrderFailedEvent

	err := json.Unmarshal(msg.Body, &event)
	if err != nil {
		log.Print(err)
		msg.Nack(false, true)
		return
	}

	log.Println("Raw message:", string(msg.Body))

	msg.Ack(false)
}

func ConsumeOrderFailedEvent() {
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

	for msg := range msgs {
		handleOrderFailedEvent(msg)
	}
}
