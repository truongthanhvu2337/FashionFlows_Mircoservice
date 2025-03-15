package config

import (
	"log"

	"github.com/streadway/amqp"
)

func ConnectRabbitMQ() (*amqp.Connection, *amqp.Channel, error) {
	rabbitMQURL := "amqp://sa:sa@rabbitmq:5672/"

	conn, err := amqp.DialConfig(rabbitMQURL, amqp.Config{
		Properties: amqp.Table{
			"connection_name": "FashionFlows.NotificationService",
		},
	})
	if err != nil {
		log.Printf("Failed to connect to RabbitMQ: %v", err)
		return nil, nil, err
	}

	ch, err := conn.Channel()
	if err != nil {
		log.Printf("Failed to create channel: %v", err)
		conn.Close()
		return nil, nil, err
	}

	return conn, ch, nil
}

