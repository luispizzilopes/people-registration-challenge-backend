import React from "react";
import { Button } from "primereact/button";
import { FaKey } from "react-icons/fa";
import history from "../../lib/history";
import { MdNoEncryptionGmailerrorred } from "react-icons/md";

const Unauthorized = () => {
  return (
    <div className="error flex justify-content-center align-items-center h-screen">
    <div className="flex-column justify-content-center text-center">
      <div className="flex-column justify-content-center text-center">
      <MdNoEncryptionGmailerrorred style={{ width: "120px", height: "120px" }}/>
        <p className="mb-1 mt-3">Acesso não autorizado.</p>
        <small>HTTP Error 401. O recurso solicitado requer autenticação do usuário.</small>
      </div>
      <div className="mt-3">
        <Button
          label="Voltar para Login"
          icon={<FaKey className="mr-3" />}
          onClick={() => history.push("/")}
        />
      </div>
    </div>
  </div>
  );
};

export default Unauthorized;
