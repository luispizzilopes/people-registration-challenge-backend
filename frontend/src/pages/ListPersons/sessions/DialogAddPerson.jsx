import React, { useState } from 'react';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import { Calendar } from 'primereact/calendar';
import { Dropdown } from 'primereact/dropdown';
import { Button } from 'primereact/button';
import { Genders } from '../../../data/gender';
import addPerson from "../features/addPerson";

export default function DialogAddPerson({ visible, setVisible, setPersons }) {
    const [person, setPerson] = useState({
        name: '',
        birthDate: null,
        cpf: '',
        address: '',
        gender: null,
        email: '',
        naturality: '',
        nacionality: '',
    });

    const [errors, setErrors] = useState({});

    const handleChange = (e) => {
        const { name, value } = e.target;
        setPerson({ ...person, [name]: value });
        setErrors({ ...errors, [name]: '' }); // limpa erro ao digitar
    };

    const validate = () => {
        const newErrors = {};
        if (!person.name.trim()) newErrors.name = 'Nome é obrigatório.';
        if (!person.birthDate) newErrors.birthDate = 'Data de nascimento é obrigatória.';
        if (!person.cpf.trim()) newErrors.cpf = 'CPF é obrigatório.';
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSave = async () => {
        if (!validate()) return;
        await addPerson(person, setVisible, setPersons);
    };

    return (
        <div>
            <Dialog header="Adicionar Pessoa" visible={visible} style={{ width: '40vw' }} onHide={() => setVisible(false)} modal>
                <div className="p-fluid grid formgrid">

                    <div className="field col-12">
                        <label htmlFor="name">Nome <strong>*</strong></label>
                        <InputText id="name" name="name" value={person.name} onChange={handleChange} maxLength={200} />
                        {errors.name && <small className="p-error">{errors.name}</small>}
                    </div>

                    <div className="field col-12">
                        <label htmlFor="birthDate">Data de Nascimento <strong>*</strong></label>
                        <Calendar locale='ptbr' id="birthDate" value={person.birthDate} onChange={(e) => {
                            setPerson({ ...person, birthDate: e.value });
                            setErrors({ ...errors, birthDate: '' });
                        }} showIcon dateFormat="dd/mm/yy" />
                        {errors.birthDate && <small className="p-error">{errors.birthDate}</small>}
                    </div>

                    <div className="field col-12">
                        <label htmlFor="cpf">CPF <strong>*</strong></label>
                        <InputText id="cpf" name="cpf" value={person.cpf} onChange={handleChange} maxLength={11} />
                        {errors.cpf && <small className="p-error">{errors.cpf}</small>}
                    </div>

                    <div className="field col-12">
                        <label htmlFor="address">Endereço</label>
                        <InputText id="address" name="address" value={person.address} onChange={handleChange} maxLength={200} />
                    </div>

                    <div className="field col-12">
                        <label htmlFor="gender">Gênero</label>
                        <Dropdown
                            id="gender"
                            name="gender"
                            value={person.gender}
                            options={Genders}
                            onChange={(e) => setPerson({ ...person, gender: e.value })}
                            placeholder="Selecione"
                        />
                    </div>

                    <div className="field col-12">
                        <label htmlFor="email">Email</label>
                        <InputText id="email" name="email" value={person.email} onChange={handleChange} maxLength={200} />
                    </div>

                    <div className="field col-12">
                        <label htmlFor="naturality">Naturalidade</label>
                        <InputText id="naturality" name="naturality" value={person.naturality} onChange={handleChange} maxLength={100} />
                    </div>

                    <div className="field col-12">
                        <label htmlFor="nacionality">Nacionalidade</label>
                        <InputText id="nacionality" name="nacionality" value={person.nacionality} onChange={handleChange} maxLength={100} />
                    </div>

                    <div className="field col-12">
                        <Button label="Salvar" icon="pi pi-check" onClick={handleSave} />
                    </div>
                </div>
            </Dialog>
        </div>
    );
}
