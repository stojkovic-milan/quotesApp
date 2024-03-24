import { FC } from "react"
import QuotesContainer from "../componenets/quotesContainer/QuotesContainer";

const Quotes: FC = () => {
    return (
        <div className="bg-gray-600 h-full w-full font-roboto">
            <div className='w-full flex justify-center items-center pt-4  flex-col gap-2'>
                <h1 className='text-5xl text-white '>Quotes</h1>
                <QuotesContainer />
            </div>
        </div>)
}
export default Quotes;