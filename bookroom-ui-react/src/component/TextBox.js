import React, { useState } from 'react'
import TextField from '@material-ui/core/TextField'


export default function TextBox({id, value, label, onChange, fullWidth, type, inputLabelProps, disabled, error, helperText})
{    
    return (
        <TextField 
            id={id} 
            label={label}
            variant="outlined" 
            onChange={onChange} 
            value={value} 
            fullWidth={fullWidth} 
            type={type} 
            InputLabelProps={inputLabelProps} 
            disabled={disabled} 
            error={error}
            helperText={helperText}/>
    );
}