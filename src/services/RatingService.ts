import { QuoteDisplayDTO, RatingDTO } from "../models/DTOs";
import { axiosInstance } from "../utils/http";

export const RatingService = {
    rateQuote: (rating: RatingDTO) => {
        return axiosInstance.post<QuoteDisplayDTO>("/rating", { ...rating })
    }
};
