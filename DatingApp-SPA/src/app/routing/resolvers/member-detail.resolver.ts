import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot } from '@angular/router';
import { catchError } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

import { User } from 'src/app/shared/models/user';
import { UserService } from 'src/app/shared/services/user.service';
import { AlertifyService } from 'src/app/shared/services/alertify.service';

@Injectable()
export class MemberDetailResolver implements Resolve<User> {

   constructor(private userService: UserService, private router: Router, private alertify: AlertifyService) { }

   resolve(route: ActivatedRouteSnapshot): Observable<User> {
      return this.userService.getUser(route.params['id'])
         .pipe(
            catchError(error => {
               this.alertify.error('Problem retrieving data');
               this.router.navigate(['/members']);
               return of(null);
            })
         );
   }
}
