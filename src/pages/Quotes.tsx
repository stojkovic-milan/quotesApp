import { FC, useEffect, useState } from "react"
import QuoteDisplay from "../componenets/quoteDisplay/QuoteDisplay"
import { QuoteService } from "../services/QuoteService"
import axios from "axios"
import { QuoteGetDTO, RatingDTO } from "../models/DTOs"
import { RatingService } from "../services/RatingService"
import { Pagination } from "../models/Pagination"
import PaginationInput from "../componenets/paginationInput/PaginationInput"

const Quotes: FC = () => {
    const [quotes, setQuotes] = useState<Array<QuoteGetDTO>>([])
    const [pagination, setPagination] = useState<Pagination>()
    //TODO: ??Add QuotesContainer Componenet, to hold logic and display quotes??
    const handleQuoteRate = async (rating: RatingDTO) => {
        const { data: response, status } = await RatingService.rateQuote(rating);
        setQuotes(prev =>
            prev.map(q => {
                if (q.quote.id == rating.quoteId)
                    return { ...response }
                else
                    return q
            })
        )
    }
    const fetchQuotes = async () => {
        try {
            const { data: response, status } = await QuoteService.getQuotes(pagination?.currentPage);
            setQuotes(response.quotes);
            if (!pagination)
                setPagination({ ...response });
        } catch (error) {
            if (axios.isAxiosError(error)) console.error(error.message);
        }
    };
    useEffect(() => {
        fetchQuotes();
    }, [])

    useEffect(() => {
        pagination && fetchQuotes();
    }, [pagination?.currentPage])

    return (
        <div className="bg-gray-600 h-screen w-full ">
            <div className='w-full flex justify-center p-4 text-white'>
                <h1 className='text-5xl'>Quotes</h1>
            </div>
            <div className='w-full flex flex-col items-center p-10 px-12 gap-6'>
                {quotes && quotes.length > 0 &&
                    quotes.map((q, ind) =>
                        <QuoteDisplay key={ind} quoteDto={q} handleQuoteRate={handleQuoteRate} />
                    )}
            </div>
            <div className="pt-4">
                {pagination && <PaginationInput pagination={pagination} setPagination={setPagination} />}
            </div>
        </div>)
}
export default Quotes;