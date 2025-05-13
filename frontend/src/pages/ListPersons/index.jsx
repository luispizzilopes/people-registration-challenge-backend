import { useEffect, useState } from "react";
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import { InputText } from 'primereact/inputtext';
import DefaultPage from "../../components/DefaultPage";
import getPersons from "./features/GetPersons";
import moment from "moment";
import { GenderOptions } from "../../data/gender";
import DialogAddPerson from "./sessions/DialogAddPerson";
import DialogDeletePerson from "./sessions/DialogDeletePerson";
import DialogUpdatePerson from "./sessions/DialogUpdatePerson";

import "./style.css";

export default function ListPersons() {
    const [persons, setPersons] = useState([]);
    const [globalFilter, setGlobalFilter] = useState('');
    const [selectedField, setSelectedField] = useState('name');

    const [openDialogAddPerson, setOpenDialogAddPerson] = useState(false);
    const [openDialogDeletePerson, setOpenDialogDeletePerson] = useState(false);
    const [openDialogUpdatePerson, setOpenDialogUpdatePerson] = useState(false);

    const [idPersonSelected, setIdPersonSelected] = useState(null);

    const fields = [
        { label: 'Nome', value: 'name' },
        { label: 'Data de Nascimento', value: 'birthDate' },
        { label: 'CPF', value: 'cpf' },
        { label: 'Endereço', value: 'address' },
        { label: 'Email', value: 'email' },
        { label: 'Naturalidade', value: 'naturality' },
        { label: 'Nacionalidade', value: 'nacionality' },
    ];

    useEffect(() => {
        getPersons(setPersons);
    }, []);

    const actionTemplate = (rowData) => {
        return (
            <>
            <Button 
                onClick={() => {
                    setIdPersonSelected(rowData.id);
                    setOpenDialogUpdatePerson(true);
                }}
                icon="pi pi-pencil" 
                rounded 
                outlined 
                 className="mb-2 md:mb-0 md:mr-2"
            />
            <Button
                onClick={() => {
                    setIdPersonSelected(rowData.id);
                    setOpenDialogDeletePerson(true);
                }}
                icon="pi pi-trash"
                rounded
                outlined
            />
        </>
        
        );
    };

    const filteredPersons = Array.isArray(persons)
        ? persons.filter(person =>
            person[selectedField]?.toString().toLowerCase().includes(globalFilter.toLowerCase())
        )
        : [];

    return (
        <DefaultPage showGridlines title={"Listagem de Pessoas"} icon={<i className="pi pi-list"></i>}>
            <div
                id="filter-persons"
                className="flex flex-column sm:flex-row justify-content-between align-items-start sm:align-items-center gap-2 mb-3"
            >
                <div className="flex flex-column sm:flex-row gap-2 w-full sm:w-auto">
                    <Dropdown
                        value={selectedField}
                        options={fields}
                        onChange={(e) => setSelectedField(e.value)}
                        placeholder="Filtrar por"
                        className="w-full sm:w-15rem"
                    />
                    <InputText
                        value={globalFilter}
                        onChange={(e) => setGlobalFilter(e.target.value)}
                        placeholder="Digite para filtrar"
                        className="w-full sm:w-20rem"
                    />
                </div>

                <Button
                    label="Adicionar Pessoa"
                    icon="pi pi-plus"
                    onClick={() => setOpenDialogAddPerson(true)}
                    className="w-full sm:w-auto"
                />
            </div>

            <DataTable
                value={filteredPersons}
                responsiveLayout="scroll"
                rowsPerPageOptions={[5, 10, 25, 50]}
                paginator
                rows={10}
                emptyMessage="Nenhuma pessoa cadastrada."
            >
                <Column field="name" header="Nome" />
                <Column
                    field="birthDate"
                    header="Data de Nascimento"
                    body={(rowData) => (
                        <p>{moment(rowData.birthDate).format('DD/MM/YYYY')}</p>
                    )}
                />
                <Column field="cpf" header="CPF" />
                <Column field="address" header="Endereço" />
                <Column field="gender" header="Gênero" body={(rowData) => GenderOptions[rowData.gender]} />
                <Column field="email" header="Email" />
                <Column field="naturality" header="Naturalidade" />
                <Column field="nacionality" header="Nacionalidade" />
                <Column header="Ações" body={actionTemplate} style={{ textAlign: 'center' }} />
            </DataTable>

            {openDialogAddPerson && (
                <DialogAddPerson
                    visible={openDialogAddPerson}
                    setVisible={setOpenDialogAddPerson}
                    setPersons={setPersons}
                />
            )}

            {openDialogDeletePerson && (
                <DialogDeletePerson
                    visible={openDialogDeletePerson}
                    setVisible={setOpenDialogDeletePerson}
                    setPersons={setPersons}
                    personId={idPersonSelected}
                />
            )}

            {openDialogUpdatePerson && (
                <DialogUpdatePerson
                    visible={openDialogUpdatePerson}
                    setVisible={setOpenDialogUpdatePerson}
                    setPersons={setPersons}
                    personId={idPersonSelected}
                />
            )}
        </DefaultPage>
    );
}
