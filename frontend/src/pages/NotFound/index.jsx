import React from "react";
import { Button } from "primereact/button";
import { IoReturnUpBack } from "react-icons/io5";
import history from "../../lib/history";
import { ImConfused } from "react-icons/im";

const NotFound = () => {
    return (
        <div className="error flex justify-content-center align-items-center h-screen">
            <div className="flex-column justify-content-center text-center">
                <div className="flex-column justify-content-center text-center">
                    <ImConfused style={{ width: "120px", height: "120px" }} />
                    <p className="mb-1 mt-3">Não Encontrado.</p>
                    <small>HTTP Error 404. O recurso solicitado não pôde ser encontrado neste servidor.</small>
                </div>
                <div className="mt-3">
                    <Button
                        label="Voltar para página Home"
                        icon={<IoReturnUpBack className="mr-3" />}
                        onClick={() => history.push("/listPersons")}
                    />
                </div>
            </div>
        </div>
    );
};

export default NotFound;
