export interface UserProfile {
    id: number;
    username: string;
    email: string;
    role: string;
    bio: string | null;
    avatarUrl: string | null;
    createdAt: string;
    totalComments: number;
    totalLikes: number;
    totalBookmarks: number;
}

export interface ChangePasswordRequest {
    currentPassword: string;
    newPassword: string;
}

export interface UpdateProfileRequest {
    username?: string;
    bio?: string;
    avatarUrl?: string;
}