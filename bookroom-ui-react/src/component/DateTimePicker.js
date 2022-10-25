import 'date-fns';
import React from 'react';
import DateFnsUtils from '@date-io/date-fns';
import {
  MuiPickersUtilsProvider,
  KeyboardTimePicker,
  KeyboardDatePicker,
} from '@material-ui/pickers';



export default function DateTimePicker({handleDateChange, value, label, type, readyOnly, required}) {
 
 function DateComponent()
 {
   return (
     <MuiPickersUtilsProvider utils={DateFnsUtils}>
         <KeyboardDatePicker
           margin="normal"
           id="date-picker-dialog"
           label={label}
           format="dd/MM/yyyy"
           value={value}
           onChange={handleDateChange}
           disabled={readyOnly}
           required={required}
           KeyboardButtonProps={{
             'aria-label': 'change date',
           }}
         />
     </MuiPickersUtilsProvider>
   );
 }
 
 function DateTimeComponent()
 {
   return(
     <MuiPickersUtilsProvider utils={DateFnsUtils}>
         <KeyboardDatePicker
           margin="normal"
           id="date-picker-dialog"
           label={label}
           format="dd/MM/yyyy"
           value={value}
           onChange={handleDateChange}
           disabled={readyOnly}
           required={required}
           KeyboardButtonProps={{
             'aria-label': 'change date',
           }}
         />
         <KeyboardTimePicker
           margin="normal"
           id="time-picker"
           label={label}
           value={value}
           onChange={handleDateChange}
           disabled={readyOnly}
           required={required}
           KeyboardButtonProps={{
             'aria-label': 'change time',
           }}
         />
     </MuiPickersUtilsProvider>
   );
 }

 if(type==="Date")
 {
  return DateComponent();
 }else{
  return DateTimeComponent();
 }
  
}