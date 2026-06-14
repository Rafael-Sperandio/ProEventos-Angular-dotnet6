import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from '@environments/environment';
import { User } from '@app/models/identity/User';
import { map, take } from 'rxjs/operators';
import { UserUpdate } from '../models/identity/UserUpdate';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User| null>(1);
  public currentUser$ = this.currentUserSource.asObservable();

    baseUrl = environment.apiURL + 'api/account/'
constructor(private http: HttpClient) { }

  public login(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'login', model).pipe(
        take(1),
        map((response: User) => {
          const user = response;
          if (user) {
            this.setCurrentUser(user)
          }
        })
    );
  }
  public getUser(): Observable<UserUpdate> {
    return this.http.get<UserUpdate>(this.baseUrl + 'getUser').pipe(take(1));
  }
  updateUser(model: UserUpdate): Observable<void> {
    return this.http.put<UserUpdate>(this.baseUrl + 'updateUser', model).pipe(
      take(1),
      map((user: UserUpdate) => {
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );
  }

  public register(model: any): Observable<void> {
    return this.http.post<User>(this.baseUrl + 'register', model).pipe(
      take(1),
      map((response: User) => {
        const user = response;
        if (user) {
          this.setCurrentUser(user)
        }
      })
    );
  }

  logout(): void {
    console.log("logout")
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
    this.currentUserSource.complete();
    /*
  Ao completar o Subject, ele deixa de emitir valores para sempre.
  Se o usuário fizer login novamente durante a mesma execução da aplicação,
  você não conseguirá emitir um novo usuário.
    */
  }


  public setCurrentUser(user: User): void {
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }



}
