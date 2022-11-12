import {
  Button,
  ButtonVariant,
  castEnumToOptions,
  FormikCheckbox,
  FormikDropdown,
  FormikText,
} from 'components';
import { Formik } from 'formik';
import { IUserModel, useApi, UserTypes } from 'hooks';
import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { IUser } from '.';
import * as styled from './styled';
import { defaultUser, toForm, toModel } from './utils';

/**
 * User component properties.
 */
export interface IUserProps {
  /**
   * Primary key to identify the user.
   */
  id?: number;
}

export const User: React.FC<IUserProps> = ({ id }) => {
  const params = useParams();
  id = parseInt(params.id ?? `${id}`);
  const api = useApi();
  const [user, setUser] = React.useState<IUser>(defaultUser);
  const navigate = useNavigate();

  React.useEffect(() => {
    if (id) {
      api.users.get(id).then((data) => {
        setUser(toForm(data));
      }); // TODO: Handle error.
    } else {
      setUser(defaultUser);
    }
  }, [api, id]);

  const handleDelete = async () => {
    await api.users.remove(toModel(user));
    await new Promise((r) => setTimeout(r, 30 * 1000));
    navigate('/admin/users');
  };

  return (
    <styled.User>
      <div>
        <h1>{id === 0 && 'Add '}User</h1>
        <div>
          {id !== 0 && (
            <Button variant={ButtonVariant.success} onClick={() => navigate('/admin/users/0')}>
              Add New
            </Button>
          )}
        </div>
      </div>
      <div>
        <Formik
          enableReinitialize
          initialValues={user}
          validate={(values) => {
            const errors = {} as any;
            if (!values.username) errors.username = 'Required';
            return errors;
          }}
          onSubmit={async (values, { setSubmitting }) => {
            try {
              let data: IUserModel;
              if (values.id === 0) {
                data = await api.users.add(toModel(values));
              } else {
                data = await api.users.update(toModel(values));
              }
              setUser(toForm(data));
              navigate(`/admin/users/${data.id}`); // TODO: Find a way to update route without refreshing page.
            } catch (error: any) {
              // TODO: Update form to show appropriate error information.
            } finally {
              setSubmitting(false);
            }
          }}
        >
          {({ values, handleSubmit, isSubmitting, setSubmitting }) => (
            <form onSubmit={handleSubmit} autoComplete="off">
              <div>
                <FormikText
                  name="username"
                  label="Username:"
                  value={values.username}
                  required
                  autoComplete="off"
                ></FormikText>
                <FormikText
                  type="email"
                  name="email"
                  label="Email:"
                  value={values.email}
                  required
                ></FormikText>
                <FormikText name="key" label="Key:" value={values.key} disabled={true}></FormikText>
                <FormikText
                  name="displayName"
                  label="Display Name:"
                  value={values.displayName}
                  required
                ></FormikText>
                <FormikText
                  name="firstName"
                  label="First Name:"
                  value={values.firstName}
                ></FormikText>
                <FormikText
                  name="middleName"
                  label="Middle Name:"
                  value={values.middleName}
                ></FormikText>
                <FormikText name="lastName" label="Last Name:" value={values.lastName}></FormikText>
                <FormikCheckbox
                  name="isEnabled"
                  label="Enabled:"
                  checked={values.isEnabled}
                ></FormikCheckbox>
                <FormikCheckbox
                  name="emailVerified"
                  label="Verified:"
                  checked={values.emailVerified}
                ></FormikCheckbox>
                <FormikText
                  type="date"
                  name="verifiedOn"
                  label="Verified On:"
                  value={`${values.verifiedOn}`}
                ></FormikText>
                <FormikDropdown
                  name="userType"
                  label="Type:"
                  required
                  options={castEnumToOptions(UserTypes)}
                ></FormikDropdown>
              </div>
              <div>
                <Button type="submit" variant={ButtonVariant.primary} disabled={isSubmitting}>
                  Save
                </Button>
                {id !== 0 && (
                  <Button
                    variant={ButtonVariant.danger}
                    onClick={async () => {
                      setSubmitting(true);
                      await handleDelete();
                      setSubmitting(false);
                    }}
                    disabled={isSubmitting}
                  >
                    Delete
                  </Button>
                )}
                <Button
                  variant={ButtonVariant.secondary}
                  onClick={() => navigate('/admin/users')}
                  disabled={isSubmitting}
                >
                  Cancel
                </Button>
              </div>
            </form>
          )}
        </Formik>
      </div>
    </styled.User>
  );
};
