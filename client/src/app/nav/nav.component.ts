import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { User } from '../_models/user';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model : any = {}

  constructor(public accountService : AccountService , private route: Router , private toastr : ToastrService) { }

  ngOnInit(): void {
  }

  login()
  {
    this.accountService.login(this.model).subscribe({
      next : _ => {
        this.route.navigateByUrl('/members');
        this.model = {};
      }
    })
  }

  logout()
  {
    this.accountService.logout();
    this.route.navigateByUrl('/');
  }

  // getCurrentUser()
  // {
  //   this.accountService.currentUser$.subscribe(user => {
  //     this.loggedIn = !!user;
  //   },error =>
  //   {
  //     console.log(error);
  //   })
  // }


}
