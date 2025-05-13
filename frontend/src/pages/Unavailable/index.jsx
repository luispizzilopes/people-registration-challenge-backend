import { Button } from "primereact/button";
import React from "react";
import { FaServer } from "react-icons/fa";
import history from "../../lib/history";

function Unavailable() {
    return (
        <div className="error flex justify-content-center align-items-center h-screen">
            <div className="flex-column justify-content-center text-center">
                <div className="flex-column justify-content-center text-center">
                    <FaServer style={{ width: "120px", height: "120px" }} />
                    <p className="mb-1 mt-3">Erro no servidor.</p>
                    <small>HTTP Error 503. O servidor está temporariamente indisponível. Tente novamente mais tarde.</small>
                </div>
                <div className="mt-3">
                    <Button
                        label="Tentar novamente"
                        icon="pi pi-refresh"
                        onClick={() => history.push("/")}
                    />
                </div>
            </div>
        </div>
    );
}

export default Unavailable; 