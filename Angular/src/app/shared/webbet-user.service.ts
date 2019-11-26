import { Injectable } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { WebbetUserDetails } from './webbet-user-details.model';

@Injectable({
  providedIn: 'root'
})
export class WebbetUserService {

  constructor( private fb : FormBuilder ) {  }

  userDetails : WebbetUserDetails = new WebbetUserDetails('', '', 0, '');

  userRegistrationFormModel = this.fb.group({
    UserName : ['', Validators.required],
    Email: ['', Validators.email],
    FullName: [''],
    Passwords : this.fb.group({
       Password:['', [Validators.required, Validators.minLength(4)]],
       ConfirmPassword:['', Validators.required]
    }, { validator : this.comparePaasswords })
  });

  userLoginFormModel = this.fb.group({
    UserName:'',
    Password:''
  })

  

  comparePaasswords(fb:FormGroup){
    let confirmPasswordCtrl = fb.get('ConfirmPassword');

    if(confirmPasswordCtrl.errors == null || 'passwordMismatch' in confirmPasswordCtrl.errors){

      if(fb.get('Password').value != confirmPasswordCtrl.value) confirmPasswordCtrl.setErrors({ passwordMismatch : true });
      else confirmPasswordCtrl.setErrors(null);
    }
  }
  
}
