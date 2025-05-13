import { Button } from "primereact/button";
import { Menubar } from "primereact/menubar";
import signOut from "../../utils/signOut";

export default function Header() {
    return (
        <div className="px-1 py-1 w-full">
            <Menubar
                model={[]}
                start={
                    <div className="flex align-items-center justify-center w-full ml-2">
                        <i className="pi pi-users text-2xl mr-2" style={{ color: 'var(--color-primary)' }}></i>
                        <p className="text-xs md:text-lg m-0">Sistema de Cadastro de Pessoas</p>
                    </div>
                }
                end={
                    <div className="flex items-center gap-2 mr-2">
                        <Button size="small" rounded outlined icon="pi pi-sign-out" onClick={()=> signOut()} />
                    </div>
                }
                className="shadow-1"
                style={{ height: '75px', background: 'var(--surface-a)', width: '100%' }}
            />
        </div>
    );
}
