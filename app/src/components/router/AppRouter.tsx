import { PrivateRoute } from 'components';
import {
  Account,
  Accounts,
  Calendar,
  Calendars,
  Draft,
  Home,
  Login,
  NotFound,
  Schedule,
  User,
  Users,
} from 'features';
import { Role } from 'hooks';
import { Route, Routes } from 'react-router-dom';

/**
 * AppRouter provides a SPA router to manage routes.
 * @returns AppRouter component.
 */
export const AppRouter = () => {
  return (
    <Routes>
      <Route path="/login" element={<Login />}></Route>
      <Route path="/calendar" element={<PrivateRoute element={<Calendar />} />} />
      <Route path="/schedule" element={<PrivateRoute element={<Schedule />} />} />
      <Route
        path="/admin/calendars"
        element={<PrivateRoute roles={Role.administrator} element={<Calendars />} />}
      />
      <Route
        path="/admin/accounts"
        element={<PrivateRoute roles={Role.administrator} element={<Accounts />} />}
      />
      <Route
        path="/admin/accounts/:id"
        element={<PrivateRoute roles={Role.administrator} element={<Account />} />}
      />
      <Route
        path="/admin/users"
        element={<PrivateRoute roles={Role.administrator} element={<Users />} />}
      />
      <Route
        path="/admin/users/:id"
        element={<PrivateRoute roles={Role.administrator} element={<User />} />}
      />
      <Route
        path="/admin/roles"
        element={<PrivateRoute roles={Role.administrator} element={<Draft />} />}
      />
      <Route path="/" element={<Home />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};
