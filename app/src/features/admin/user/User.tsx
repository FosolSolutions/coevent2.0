import {
  Button,
  ButtonVariant,
  castEnumToOptions,
  Col,
  FormikCheckbox,
  FormikDropdown,
  FormikText,
  Row,
} from 'components';
import { Formik } from 'formik';
import { IRoleModel, IUserModel, useApi, usePadlock, UserClaim, UserStatus, UserType } from 'hooks';
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
  const api = useApi();
  const navigate = useNavigate();
  const padlock = usePadlock();

  const [user, setUser] = React.useState<IUser>(defaultUser);
  const [roles, setRoles] = React.useState<IRoleModel[]>([]);

  id = parseInt(params.id ?? `${id}`);

  React.useEffect(() => {
    api.roles.getPage({ quantity: 100 }).then((data) => {
      setRoles(data.items);
    });
  }, [api.roles]);

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
          {({ values, handleSubmit, isSubmitting, setSubmitting, setFieldValue, handleChange }) => (
            <form onSubmit={handleSubmit} autoComplete="off">
              <Col>
                <Row>
                  <FormikText
                    name="username"
                    label="Username:"
                    required
                    autoComplete="off"
                  ></FormikText>
                  <FormikText type="email" name="email" label="Email:" required></FormikText>
                  <FormikCheckbox name="emailVerified" label="Email Verified:"></FormikCheckbox>
                  <FormikCheckbox name="isEnabled" label="Enabled:"></FormikCheckbox>
                </Row>
                <Row>
                  <FormikText name="displayName" label="Display Name:" required></FormikText>
                  <FormikText name="key" label="Key:" disabled={true}></FormikText>
                </Row>
                <Row>
                  <FormikText name="firstName" label="First Name:"></FormikText>
                  <FormikText name="middleName" label="Middle Name:"></FormikText>
                  <FormikText name="lastName" label="Last Name:"></FormikText>
                </Row>
                <Row>
                  <FormikDropdown
                    name="status"
                    label="Status:"
                    required
                    options={castEnumToOptions(UserStatus)}
                  ></FormikDropdown>
                  <FormikDropdown
                    name="userType"
                    label="Type:"
                    required
                    options={castEnumToOptions(UserType)}
                  ></FormikDropdown>
                </Row>
                <Row>
                  <FormikDropdown
                    name="roles"
                    label="Roles:"
                    optionValue="id"
                    optionText="name"
                    multiple
                    options={roles}
                  ></FormikDropdown>
                  <FormikDropdown
                    name="attributes"
                    label="Attributes:"
                    multiple
                    options={castEnumToOptions(UserClaim)}
                    value={values.claims.filter((c) => c.name === 'attribute').map((c) => c.value)}
                    onChange={(e) => {
                      const values = [...e.currentTarget.selectedOptions].map((o) => ({
                        userId: padlock.identity?.uid,
                        accountId: 1,
                        name: 'attribute',
                        value: o.value,
                      }));
                      setFieldValue('claims', values);
                      handleChange(e);
                    }}
                  ></FormikDropdown>
                </Row>
              </Col>
              <Row>
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
              </Row>
            </form>
          )}
        </Formik>
      </div>
    </styled.User>
  );
};
