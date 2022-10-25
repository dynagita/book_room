import React from "react";
import AlertDialog from "./component/AlertDialog";
import ToastDialog from "./component/ToastDialog";
import { DialogProvider } from "./context/DialogContext";
import { ToastProvider } from "./context/ToastDialogContext";
import { UserProvider } from "./context/UserContext";
import Routes from "./Routes";

const App = () => {    
    return (<UserProvider>
                <DialogProvider>
                    <ToastProvider>
                        <AlertDialog></AlertDialog>
                        <ToastDialog></ToastDialog>
                        <Routes />
                    </ToastProvider>
                </DialogProvider>
            </UserProvider>);
}
export default App;