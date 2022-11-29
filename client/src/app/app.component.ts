import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  //we can pass data from component to view i.e., html called as interpolation.
  title = 'The Dating Application';
  users : any;

  constructor(private http: HttpClient){}
  ngOnInit()
  {
    this.getUsers();
  }
  getUsers()
  {
    this.http.get('https://localhost:5001/api/users').subscribe(response =>
    {
      this.users = response;
    },error =>{
      console.log(error);
    })
  }



  

}
