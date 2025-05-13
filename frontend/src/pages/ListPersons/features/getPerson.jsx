import { toast } from "react-toastify";
import api from "../../../services/api";

export default async function getPerson(setPerson, personId) {
    try {
        const response = await api.get(`v1/Person/${personId}`);

        setPerson(response.data.value);
    } catch {
        toast.error("Ocorreu um erro ao tentar carregar a pessoa informada!");
    }
}
