export interface CommentResponse {
    id: number;
    body: string;
    username: string;
    userId: number;
    postId: number;
    createdAt: string;
}

export interface CreateCommentRequest {
    body: string;
    postId: number;
}