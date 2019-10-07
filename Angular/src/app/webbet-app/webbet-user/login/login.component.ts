import { WebbetAppService } from './../../../shared/webbet-app.service';
import { Component, OnInit } from '@angular/core';
import { WebbetUserService } from 'src/app/shared/webbet-user.service';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styles: []
})
export class LoginComponent implements OnInit {

  constructor( public userService: WebbetUserService , 
               public appService : WebbetAppService, 
               private router: Router, 
               private toastr: ToastrService ) { }

  ngOnInit() {
    if(localStorage.getItem('token') != null)

        this.router.navigate(['/offer']);
  }

  onSubmit(form: NgForm){
    this.appService.login(form.value).subscribe( (res : any) => {

      localStorage.setItem('token', res.token);
      form.reset();
      this.router.navigate(['/offer']);
    },
    err=> {
      //BadRequest
        if(err.status == 400) 
            this.toastr.error('Neispravano korisničko ime ili lozinka', 'Neuspjela prijava');
        else 
          console.log(err);
        }
    );
  }
}
