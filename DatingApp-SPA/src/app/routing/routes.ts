import { Routes } from '@angular/router';

import { HomeComponent } from '../home/home.component';
import { MemberListComponent } from '../members/member-list/member-list.component';
import { MemberDetailComponent } from '../members/member-detail/member-detail.component';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';
import { MessagesComponent } from '../messages/messages.component';
import { ListsComponent } from '../lists/lists.component';

import { AuthGuard } from './guards/auth.guard';
import { MemberDetailResolver } from './resolvers/member-detail.resolver';
import { MemberListResolver } from './resolvers/member-list.resolver';

export const appRoutes: Routes = [
   {
      path: '',
      component: HomeComponent
   },
   {
      path: '',
      runGuardsAndResolvers: 'always',
      canActivate: [AuthGuard],
      children: [
         {
            path: 'members',
            component: MemberListComponent,
            resolve: { users: MemberListResolver }
         },
         {
            path: 'members/:id',
            component: MemberDetailComponent,
            resolve: { user: MemberDetailResolver }
         },
         {
            path: 'profile',
            component: MemberEditComponent
         },
         {
            path: 'messages',
            component: MessagesComponent
         },
         {
            path: 'lists',
            component: ListsComponent
         }
      ]
   },
   {
      path: '**',
      redirectTo: '',
      pathMatch: 'full'
   }
];
