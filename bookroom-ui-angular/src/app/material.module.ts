import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatStepperModule } from '@angular/material/stepper';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

// Import other Angular Material modules as needed

@NgModule({
  imports: [
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    // Add other Angular Material modules as needed
  ],
  exports: [
    MatButtonModule,
    MatIconModule,
    MatInputModule,
    MatStepperModule,
    MatProgressSpinnerModule
    // Add other Angular Material modules as needed
  ]
})
export class MaterialModule { }
