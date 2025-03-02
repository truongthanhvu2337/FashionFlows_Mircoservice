package config

import (
	"log"

	"github.com/streadway/amqp"
)

// Kết nối RabbitMQ
func ConnectRabbitMQ() (*amqp.Connection, *amqp.Channel) {
	conn, err := amqp.Dial("amqp://sa:sa@rabbitmq:5672/")
	if err != nil {
		log.Println(err)
	}

	ch, err := conn.Channel()
	if err != nil {
		log.Println(err)
	}

	return conn, ch
}
