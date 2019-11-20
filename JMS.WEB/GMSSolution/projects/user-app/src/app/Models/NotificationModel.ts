export interface NotificationModel {
    id: number
    text: string;
    isRead: boolean;
    creationTime: Date;
    userId: string;
}