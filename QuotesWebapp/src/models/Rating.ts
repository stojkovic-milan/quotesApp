import { Quote } from "./Quote";
import { User } from "./User";

export interface Rating {
    id: string;
    value: number;
    user: User;
    quote: Quote;
}