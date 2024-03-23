import { Rating } from "./Rating";

export interface Quote {
    id: string;
    content: string;
    author: string;
    tags: string[];
    ratingList: Rating[];
    positiveCount: number;
    negativeCount: number;
}