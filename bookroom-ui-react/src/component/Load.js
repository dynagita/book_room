import React from 'react';
import RotateLeftIcon from '@material-ui/icons/RotateLeft';
import { blue } from '@material-ui/core/colors';

export default function Load(){

    return(
        <div style={{minWidth: "60vh"}}>
            <div className="Loading">
                <RotateLeftIcon className="spin" style={{color: blue[500], fontSize: 70}}/>
            </div>
        </div>
    );
}