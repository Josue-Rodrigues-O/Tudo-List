export interface UpdatePassword {
    userId: number;
    currentPassword: string;
    newPassword: string;
    confirmNewPassword: string;
}