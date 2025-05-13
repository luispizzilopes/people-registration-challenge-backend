import { Button } from 'primereact/button';
import { Calendar } from 'primereact/calendar';
import { Dialog } from 'primereact/dialog';
import { Dropdown } from 'primereact/dropdown';
import { InputText } from 'primereact/inputtext';
import React, { useEffect, useState } from 'react';
import { Genders } from '../../../data/gender';
import getPerson from "../features/getPerson";
import updatePerson from '../features/updatePerson';

export default function DialogUpdatePerson({ visible, setVisible, setPersons, personId }) {
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

    useEffect(() => {
        getPerson(setPerson, personId);
    }, [])

    const handleChange = (e) => {
        const { name, value } = e.target;
        setPerson({ ...person, [name]: value });
        setErrors({ ...errors, [name]: '' });
    };

    const validar = () => {
        const newErrors = {};
        if (!person.name.trim()) newErrors.name = 'O nome é obrigatório.';
        if (!person.birthDate) newErrors.birthDate = 'A data de nascimento é obrigatória.';
        if (!person.cpf.trim()) newErrors.cpf = 'O CPF é obrigatório.';
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSalvar = async () => {
        if (!validar()) return;
        await updatePerson(person, setVisible, setPersons);
    };

    return (
        <div>
            <Dialog header="Atualizar Pessoa" visible={visible} style={{ width: '40vw' }} onHide={() => setVisible(false)} modal>
                <div className="p-fluid grid formgrid">

                    <div className="field col-12">
                        <label htmlFor="name">Nome <strong>*</strong></label>
                        <InputText id="name" name="name" value={person.name} onChange={handleChange} maxLength={200} />
                        {errors.name && <small className="p-error">{errors.name}</small>}
                    </div>

                    <div className="field col-12">
                        <label htmlFor="birthDate">Data de Nascimento <strong>*</strong></label>
                        <Calendar
                            locale="ptbr"
                            id="birthDate"
                            value={person.birthDate ? new Date(person.birthDate) : null}
                            onChange={(e) => {
                                setPerson({ ...person, birthDate: e.value });
                                setErrors({ ...errors, birthDate: '' });
                            }}
                            showIcon
                            dateFormat="dd/mm/yy"
                        />
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
                        <Button label="Salvar" icon="pi pi-check" onClick={handleSalvar} />
                    </div>
                </div>
            </Dialog>
        </div>
    );
}
