import { FC } from 'react';
import { QuoteDisplayDTO, RatingDTO } from '../../models/DTOs';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faCaretDown, faCaretUp, faL } from '@fortawesome/free-solid-svg-icons';

type QuoteDisplayProps = {
    quoteDto: QuoteDisplayDTO,
    handleQuoteRate: (ratingDto: RatingDTO) => Promise<void>
}
const QuoteDisplay: FC<QuoteDisplayProps> = ({ quoteDto, handleQuoteRate }) =>
    <div className='w-full flex flex-row h-28'>
        <div className='w-1/12 h-full flex flex-col justify-center items-center'>
            <FontAwesomeIcon icon={faCaretUp} size='xl'
                className='cursor-pointer'
                color={quoteDto.userVotePositive == true ? 'white' : 'gray'}
                onClick={() => handleQuoteRate({ positive: true, quoteId: quoteDto.quote.id })} />
            <p className='text-xl'
                style={{ color: `rgb(${(100 - quoteDto.percentage) * 255},${quoteDto.percentage * 255},0)` }}>
                {`${quoteDto.percentage}%`}
            </p>
            <p className='text-white text-sm'>
                {`${quoteDto.quote.positiveCount}/${quoteDto.quote.negativeCount}`}
            </p>
            <FontAwesomeIcon icon={faCaretDown} size='xl'
                className='cursor-pointer'
                color={quoteDto.userVotePositive == false ? 'white' : 'gray'}
                onClick={() => handleQuoteRate({ positive: false, quoteId: quoteDto.quote.id })} />

        </div>
        <div className='w-11/12 bg-white h-full rounded-lg p-4'>
            <div className='h-4/5 overflow-y-auto'>
                <span className='text-lg'>{quoteDto.quote.content}</span>
            </div>
            <div className='h-1/5 w-full flex justify-end'>
                <span className='text-lg text-gray-500'> - {quoteDto.quote.author}</span>
            </div>
        </div>

    </div>

export default QuoteDisplay