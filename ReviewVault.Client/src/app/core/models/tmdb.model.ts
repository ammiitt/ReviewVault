export interface TmdbResponse {
    results: TmdbItem[];
    total_pages: number;
    total_results: number;
}

export interface TmdbItem {
    id: number;
    title?: string;           // for movies
    name?: string;            // for TV shows
    overview: string;
    poster_path: string | null;
    backdrop_path: string | null;
    vote_average: number;
    release_date?: string;    // for movies
    first_air_date?: string;  // for TV shows
    media_type?: string;
    genre_ids: number[];
}