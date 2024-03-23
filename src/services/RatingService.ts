import { QuoteGetDTO, RatingDTO } from "../models/DTOs";
import { axiosInstance } from "../utils/http";

export const RatingService = {
    rateQuote: (rating: RatingDTO) => {
        return axiosInstance.post<QuoteGetDTO>("/rating", { ...rating })
    }
};
