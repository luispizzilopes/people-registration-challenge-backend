import { toast } from "react-toastify";
import api from "../../../services/api";
import getPersons from "./GetPersons";

export default async function updatePerson(person, setOpen, setPersons) {
    try {
        const response = await api.put("v1/Person", person);

        toast.success(response.data.successMessage);

        await getPersons(setPersons);

        setOpen(false);
    }
    catch (error) {
        toast.error(error.response.data.errorMessage ?? "Ocorreu um erro ao tentar atualizar a pessoa!");
    }
}