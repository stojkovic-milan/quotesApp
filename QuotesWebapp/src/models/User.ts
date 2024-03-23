import { Rating } from "./Rating";

export interface User {
    id: string;
    email: string;
    passwordHash: string;
    ratings: Rating[];
}