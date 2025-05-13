import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import { useState } from "react";
import createUser from "./features/createUser";

export default function Register() {
    const navigate = useNavigate();
    const [credentials, setCredentials] = useState({
        email: "",
        password: ""
    });

    return (
        <div className="h-screen flex align-items-center justify-content-center">
            <Card className="p-2 shadow-2 border-round w-full lg:w-6">
                <div className="text-center mb-5">
                    <div className="flex flex-column align-items-center">
                        <i className="pi pi-user-plus text-6xl mb-2"></i>
                        <small className="mb-3">Criação de Conta</small>
                        <span className="text-600 font-medium line-height-3">Preencha os dados para se registrar no sistema</span>
                    </div>
                </div>

                <div>
                    <label htmlFor="email" className="block text-900 mb-2">
                        <i className="pi pi-envelope"></i> E-mail
                    </label>
                    <InputText 
                        value={credentials.email} 
                        onChange={(e) => setCredentials({ ...credentials, email: e.target.value })} 
                        id="email" 
                        type="text" 
                        className="w-full mb-3"
                    />

                    <label htmlFor="password" className="block text-900 mb-2">
                        <i className="pi pi-lock"></i> Senha
                    </label>
                    <InputText
                        value={credentials.password} 
                        onChange={(e) => setCredentials({ ...credentials, password: e.target.value })}  
                        id="password" 
                        type="password" 
                        className="w-full mb-3"
                    />

                    <Button 
                        onClick={() => createUser(credentials.email, credentials.password, setCredentials)}
                        label="Registrar" 
                        icon="pi pi-plus"
                        className="w-full"
                    />
                    <Button
                        onClick={() => navigate("/")}
                        label="Voltar para tela de Login"
                        severity="secondary"
                        icon="pi pi-arrow-left"
                        className="w-full mt-2"
                    />
                </div>
            </Card>
        </div>
    );
}
