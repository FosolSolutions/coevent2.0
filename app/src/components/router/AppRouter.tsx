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
import { Claim, usePadlock } from 'hooks';
import React from 'react';
import { Route, Routes, useLocation, useNavigate } from 'react-router-dom';

/**
 * AppRouter provides a SPA router to manage routes.
 * @returns AppRouter component.
 */
export const AppRouter = () => {
  const { authenticated } = usePadlock();
  const { pathname } = useLocation();
  const [redirect, setRedirect] = React.useState<string | null>(pathname);
  const navigate = useNavigate();

  React.useEffect(() => {
    // This is required to handle direct requests through manual URL edits.
    if (authenticated && redirect && redirect !== '/login') {
      navigate(redirect);
    }
    return () => {
      // This is required due to not being able to modify state at the same time as navigating.
      if (authenticated && redirect && redirect !== '/login') {
        setRedirect(null);
        navigate(redirect);
      }
    };
  }, [authenticated, redirect, navigate]);

  return (
    <Routes>
      <Route path="/login" element={<Login />}></Route>
      <Route path="/calendar" element={<PrivateRoute element={<Calendar />} />} />
      <Route path="/schedule" element={<PrivateRoute element={<Schedule />} />} />
      <Route
        path="/admin/calendars"
        element={<PrivateRoute claims={Claim.administrator} element={<Calendars />} />}
      />
      <Route
        path="/admin/accounts"
        element={<PrivateRoute claims={Claim.administrator} element={<Accounts />} />}
      />
      <Route
        path="/admin/accounts/:id"
        element={<PrivateRoute claims={Claim.administrator} element={<Account />} />}
      />
      <Route
        path="/admin/users"
        element={<PrivateRoute claims={Claim.administrator} element={<Users />} />}
      />
      <Route
        path="/admin/users/:id"
        element={<PrivateRoute claims={Claim.administrator} element={<User />} />}
      />
      <Route
        path="/admin/roles"
        element={<PrivateRoute claims={Claim.administrator} element={<Draft />} />}
      />
      <Route path="/" element={<Home />} />
      <Route path="*" element={<NotFound />} />
    </Routes>
  );
};
