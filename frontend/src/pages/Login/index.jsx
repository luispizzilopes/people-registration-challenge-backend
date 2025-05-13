import { useState } from "react";
import { Card } from "primereact/card";
import { InputText } from "primereact/inputtext";
import { Button } from "primereact/button";
import { useNavigate } from "react-router-dom";
import signIn from "./features/signIn";

export default function Login() {
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
                        <i className="pi pi-users text-6xl mb-2"></i>
                        <small className="mb-3">Sistema de Gestão de Pessoas</small>
                        <span className="text-600 font-medium line-height-3">Informe suas credenciais para realizar a autenticação</span>
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
                        onClick={() => signIn(credentials.email, credentials.password)}
                        label="Entrar"
                        icon="pi pi-sign-in"
                        className="w-full"
                    />
                    <Button
                        onClick={() => navigate("/register")}
                        label="Registre-se"
                        severity="secondary"
                        icon="pi pi-user-plus"
                        className="w-full mt-2"
                    />
                </div>
            </Card>
        </div>
    );
}