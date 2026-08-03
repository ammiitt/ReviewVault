export interface CategoryResponse {
    id: number;
    name: string;
}

export interface MediaTypeResponse {
    id: number;
    name: string;
    description: string | null;
    isActive: boolean;
}