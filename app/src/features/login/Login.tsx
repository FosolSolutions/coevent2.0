import { Button, FormikText, Logo } from 'components';
import { Formik } from 'formik';
import { useApi, usePadlock } from 'hooks';
import React from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';

import { IParticipantLoginForm, IUserLoginForm } from '.';
import * as styled from './LoginStyled';

/**
 * Login will display content for an anonymous user.
 * If the user is already authenticated it will redirect to the home route.
 * @returns Login component.
 */
export const Login = () => {
  const padlock = usePadlock();
  const api = useApi();
  const navigate = useNavigate();
  const [params] = useSearchParams();

  const redirect_uri = params.get('redirect_uri');
  const key = params.get('key');
  const defaultParticipantValues: IParticipantLoginForm = { key: '' };
  const defaultUserValues: IUserLoginForm = { username: '', password: '' };

  React.useEffect(() => {
    // If the participant key is in query param, then login automatically.
    const login = async () => {
      if (!!key) {
        const token = await api.auth.loginAsParticipant({ key });
        padlock.login(token);
        navigate(redirect_uri ?? '/');
      }
    };
    login();
  }, [api.auth, key, navigate, padlock, redirect_uri]);

  return (
    <styled.Login>
      <div>
        <Logo />
        <p>Scheduling for teams</p>
      </div>
      <Formik
        initialValues={defaultParticipantValues}
        onSubmit={async (values) => {
          try {
            if (values.key) {
              const token = await api.auth.loginAsParticipant({ key: values.key });
              padlock.login(token);
              navigate(redirect_uri ?? '/');
            }
          } catch (error) {
            // Handle error
          }
        }}
      >
        {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
          <form id="participant" onSubmit={handleSubmit}>
            <FormikText
              name="key"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.key}
              placeholder="Paste your participant key"
            ></FormikText>
            <Button type="submit" disabled={isSubmitting || !values.key}>
              Login
            </Button>
          </form>
        )}
      </Formik>
      <div>
        <p>Or login with your user account</p>
      </div>
      <Formik
        initialValues={defaultUserValues}
        onSubmit={async (values) => {
          try {
            if (values.username && values.password) {
              const token = await api.auth.login({
                username: values.username,
                password: values.password,
              });
              padlock.login(token);
              navigate(redirect_uri ?? '/');
            }
          } catch (error) {
            // Handle error
          }
        }}
      >
        {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
          <form id="user" onSubmit={handleSubmit}>
            <FormikText
              name="username"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.username}
              placeholder="Username or Email"
            ></FormikText>
            <FormikText
              name="password"
              type="password"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.password}
              placeholder="Enter your password"
              autoComplete="on"
            ></FormikText>
            <Button type="submit" disabled={isSubmitting || !values.username || !values.password}>
              Login
            </Button>
          </form>
        )}
      </Formik>
    </styled.Login>
  );

  // return (
  //   <styled.Login>
  //     <div>
  //       <div>
  //         <Logo />
  //         <p>Scheduling for teams</p>
  //       </div>
  //       <div>
  //         <Formik
  //           initialValues={defaultParticipantValues}
  //           onSubmit={async (values) => {
  //             try {
  //               if (values.key) {
  //                 const token = await api.auth.loginAsParticipant({ key: values.key });
  //                 auth.login(token);
  //                 navigate(redirect_uri ?? '/');
  //               }
  //             } catch (error) {
  //               // Handle error
  //             }
  //           }}
  //         >
  //           {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
  //             <form id="participant" onSubmit={handleSubmit}>
  //               <FormikText
  //                 name="key"
  //                 onChange={handleChange}
  //                 onBlur={handleBlur}
  //                 value={values.key}
  //                 placeholder="Paste your participant key"
  //               ></FormikText>
  //               <Button type="submit" disabled={isSubmitting || !values.key}>
  //                 Login
  //               </Button>
  //             </form>
  //           )}
  //         </Formik>
  //       </div>
  //       <div>
  //         <p>Or login with your user account</p>
  //       </div>
  //       <div>
  //         <Formik
  //           initialValues={defaultUserValues}
  //           onSubmit={async (values) => {
  //             try {
  //               if (values.username && values.password) {
  //                 const token = await api.auth.login({
  //                   username: values.username,
  //                   password: values.password,
  //                 });
  //                 auth.login(token);
  //                 navigate(redirect_uri ?? '/');
  //               }
  //             } catch (error) {
  //               // Handle error
  //             }
  //           }}
  //         >
  //           {({ values, handleChange, handleBlur, handleSubmit, isSubmitting }) => (
  //             <form id="user" onSubmit={handleSubmit}>
  //               <FormikText
  //                 name="username"
  //                 onChange={handleChange}
  //                 onBlur={handleBlur}
  //                 value={values.username}
  //                 placeholder="Username or Email"
  //               ></FormikText>
  //               <FormikText
  //                 name="password"
  //                 type="password"
  //                 onChange={handleChange}
  //                 onBlur={handleBlur}
  //                 value={values.password}
  //                 placeholder="Enter your password"
  //                 autoComplete="on"
  //               ></FormikText>
  //               <Button
  //                 type="submit"
  //                 disabled={isSubmitting || !values.username || !values.password}
  //               >
  //                 Login
  //               </Button>
  //             </form>
  //           )}
  //         </Formik>
  //       </div>
  //     </div>
  //   </styled.Login>
  // );
};
