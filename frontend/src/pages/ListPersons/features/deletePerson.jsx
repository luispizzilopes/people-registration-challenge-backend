import { toast } from "react-toastify";
import api from "../../../services/api";
import getPersons from "./GetPersons";

export default async function deletePerson(personId, setOpen, setPersons) {
    try {
        const response = await api.delete(`v1/Person/${personId}`);

        toast.success(response.data.successMessage);

        await getPersons(setPersons);

        setOpen(false);
    }
    catch (error) {
        toast.error(error.response.data.errorMessage ?? "Ocorreu um erro ao tentar deleter a pessoa!");
    }
}