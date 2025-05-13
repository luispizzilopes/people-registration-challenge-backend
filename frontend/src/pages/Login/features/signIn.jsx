import { toast } from "react-toastify";
import api from "../../../services/api";
import history from "../../../lib/history";

export default async function signIn(email, password) {
    try {
        const response = await api.post("v1/Authentication/sign-in", {
            email,
            password
        });

        localStorage.setItem("token", response.data.value.token);
        history.push("/listPersons");
    } catch (error) {
        toast.error(error?.response?.data?.errorMessage ?? "Ocorreu um erro ao realizar a autenticação!")
    }
}
