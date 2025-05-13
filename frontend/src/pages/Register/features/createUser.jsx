import { toast } from "react-toastify";
import api from "../../../services/api";

export default async function createUser(email, password, setCredentials) {
    try {
        const response = await api.post("v1/User", {
            email,
            password
        });

        toast.success(response.data.successMessage);
        setCredentials({email: "", password: ""}); 
    } catch (error) {
        toast.error(error?.response?.data?.errorMessage ?? "Ocorreu um erro ao tentar registrar um novo usuário!")
    }
}
