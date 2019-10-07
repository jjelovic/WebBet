import { Component, OnInit } from '@angular/core';
import { WebbetUserService } from 'src/app/shared/webbet-user.service';
import { WebbetAppService } from 'src/app/shared/webbet-app.service';
import { element } from '@angular/core/src/render3/instructions';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styles: []
})
export class RegistrationComponent implements OnInit {

  constructor( public userService : WebbetUserService, public service: WebbetAppService, private tostr: ToastrService) { }

  ngOnInit() {
    this.userService.userRegistrationFormModel.reset();
  }
 
  onSubmit(){
    
    var registrationBody = {
      UserName: this.userService.userRegistrationFormModel.value.UserName,
      Email: this.userService.userRegistrationFormModel.value.Email,
      FullName: this.userService.userRegistrationFormModel.value.FullName,
      Password: this.userService.userRegistrationFormModel.value.Passwords.Password
    }
    

    this.service.register(registrationBody).subscribe(
      (res : any) => {
        if(res.succeeded){
          this.userService.userRegistrationFormModel.reset();
          this.tostr.success('Uspješno kreiran novi korisnik.','Uspješna registracija')
        }else{
          res.errors.forEach(el => {
            switch(el.code){
                case 'DuplicateUserName':
                  this.tostr.error('Korisničko ime već postoji u bazi','Neuspješna registracija')
                  break;

                default:
                  break;
            }
          });
        }
      },
      err => {
        console.log(err);
      }
    );
  }
}
