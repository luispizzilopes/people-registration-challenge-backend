import { Dialog } from 'primereact/dialog';
import { Button } from 'primereact/button';
import React from 'react';
import deletePerson from '../features/deletePerson';

export default function DialogDeletePerson({ visible, setVisible, setPersons, personId }) {
    return (
        <div>
            <Dialog
                header="Deletar Pessoa"
                visible={visible}
                style={{ width: '35vw' }}
                onHide={() => setVisible(false)}
                modal
            >
                <div className='text-center mb-3'>
                    <p className='m-0 font-medium'>Você tem certeza que deseja deletar essa pessoa?</p>
                    <small className='mb-2'>Essa ação não poderá ser desfeita.</small>

                </div>

                <div className="flex justify-content-center gap-2">
                    <Button label="Cancelar" icon="pi pi-times" onClick={() => setVisible(false)} />
                    <Button label="Deletar" icon="pi pi-trash" severity='danger' onClick={async () => deletePerson(personId, setVisible, setPersons)} />
                </div>
            </Dialog>
        </div>
    );
}
