import { FC } from "react";
import { Link } from "react-router-dom";

const Unauthorized: FC = () => {
    return (
        <div className="bg-gray-600 h-screen w-full flex-col
        font-roboto flex justify-center items-center ">
            <span className="text-white text-3xl">
                Auth token missing in Local Storage, please login.
            </span>
            <Link className="underline text-red-800 text-4xl" to='/'>Retry</Link>
        </div>
    )
}

export default Unauthorized;