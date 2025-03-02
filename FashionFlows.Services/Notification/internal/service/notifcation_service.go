package service

import (
	"notificationservice/internal/model"
	"notificationservice/internal/repository"
)

type NotificationService struct {
	repo *repository.NotificationRepository
}

func NewNotificationService(repo *repository.NotificationRepository) *NotificationService {
	return &NotificationService{repo}
}

func (s *NotificationService) CreateNotification(notification *model.Notification) error {
	return s.repo.Create(notification)
}

func (s *NotificationService) GetNotificationByID(id int) (*model.Notification, error) {
	return s.repo.GetByID(id)
}

func (s *NotificationService) GetAllNotifications() ([]model.Notification, error) {
	return s.repo.GetAll()
}

func (s *NotificationService) MarkAsRead(id int) error {
	return s.repo.UpdateIsRead(id, true)
}

func (s *NotificationService) DeleteNotification(id int) error {
	return s.repo.Delete(id)
}
