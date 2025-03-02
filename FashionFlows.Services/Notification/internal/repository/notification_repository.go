package repository

import (
	"notificationservice/internal/model"

	"gorm.io/gorm"
)

type NotificationRepository struct {
	db *gorm.DB
}

func NewNotificationRepository(db *gorm.DB) *NotificationRepository {
	return &NotificationRepository{db}
}

func (r *NotificationRepository) Create(notification *model.Notification) error {
	return r.db.Create(notification).Error
}

func (r *NotificationRepository) GetByID(id int) (*model.Notification, error) {
	var notification model.Notification
	err := r.db.First(&notification, "id = ?", id).Error
	if err != nil {
		return nil, err
	}
	return &notification, nil
}

func (r *NotificationRepository) GetAll() ([]model.Notification, error) {
	var notifications []model.Notification
	err := r.db.Find(&notifications).Error
	return notifications, err
}

func (r *NotificationRepository) UpdateIsRead(id int, isRead bool) error {
	return r.db.Model(&model.Notification{}).
		Where("id = ?", id).
		Update("is_read", isRead).Error
}

func (r *NotificationRepository) Delete(id int) error {
	return r.db.Delete(&model.Notification{}, "id = ?", id).Error
}
