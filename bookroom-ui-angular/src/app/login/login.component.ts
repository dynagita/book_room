import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Component, ViewChild, ElementRef  } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent {

  @ViewChild('loginButton') loginButton!: ElementRef;

  @ViewChild('stepper') _stepper!: MatStepper;

  firstFormGroup = this._formBuilder.group({
    firstCtrl: ['', Validators.required],
  });
  secondFormGroup = this._formBuilder.group({
    secondCtrl: ['', Validators.required],
  });
  isLinear = false;  

  constructor(private _formBuilder: FormBuilder) {}

  GoToPassword(){
    console.log(this.firstFormGroup.valid);
    if(this.firstFormGroup.valid)
      this._stepper.next();
  }

  async Login(){
    if(this.secondFormGroup.valid){
      this._stepper.next();
    }
  }
}
