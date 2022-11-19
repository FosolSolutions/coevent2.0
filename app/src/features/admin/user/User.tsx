// eslint-disable-next-line @typescript-eslint/no-unused-vars
import { AxiosError } from 'axios';
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
import { Attribute, IRoleModel, IUserModel, useApi, usePadlock, UserStatus, UserType } from 'hooks';
import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';

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
    api.roles
      .getPage({ quantity: 100 })
      .then((data) => {
        setRoles(data.items);
      })
      .catch((error: any | AxiosError) => {
        toast.error(error.message);
      });
  }, [api.roles]);

  React.useEffect(() => {
    if (id) {
      api.users
        .get(id)
        .then((data) => {
          setUser(toForm(data));
        })
        .catch((error: any | AxiosError) => {
          toast.error(error.message);
        });
    } else {
      setUser(defaultUser);
    }
  }, [api, id]);

  const handleDelete = async () => {
    try {
      await api.users.remove(toModel(user));
      navigate('/admin/users');
    } catch (ex: any | AxiosError) {
      toast.error(ex.message);
    }
  };

  const impersonate = async () => {
    if (!!user.key) {
      try {
        const token = await api.auth.loginAsParticipant({ key: user.key });
        padlock.login(token);
        navigate('/');
      } catch (ex: any | AxiosError) {
        toast.error(ex.message);
      }
    }
  };

  const invite = async () => {
    try {
      const res = await api.mail.invite(toModel(user));
      toast.success(`Invitation sent to ${res.to.email}`);
    } catch (ex: any | AxiosError) {
      toast.error(ex.message);
    }
  };

  return (
    <styled.User>
      <div>
        <Button variant={ButtonVariant.secondary} onClick={() => navigate('/admin/users')}>
          Back
        </Button>
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
                  <Button variant={ButtonVariant.warning} onClick={impersonate} className="group">
                    Impersonate
                  </Button>
                  <Button variant={ButtonVariant.info} onClick={invite} className="group">
                    Invite
                  </Button>
                </Row>
                <Row>
                  <FormikCheckbox name="isEnabled" label="Enabled:"></FormikCheckbox>
                  <FormikText
                    type="date"
                    name="emailVerifiedOn"
                    label="Email Verified On"
                  ></FormikText>
                  <FormikCheckbox name="emailVerified" label="Email Verified:"></FormikCheckbox>
                </Row>
                <Row>
                  <FormikText name="displayName" label="Display Name:" required></FormikText>
                  <FormikText name="key" label="Key:" disabled={true}></FormikText>
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
                    onChange={(e) => {
                      const values = [...e.currentTarget.selectedOptions]
                        .map((o) => {
                          return roles.find((r) => r.id === parseInt(o.value));
                        })
                        .filter((r) => !!r);
                      setFieldValue('roles', values);
                    }}
                  ></FormikDropdown>
                  <FormikDropdown
                    name="attributes"
                    label="Attributes:"
                    multiple
                    options={castEnumToOptions(Attribute)}
                    value={values.claims.filter((c) => c.name === 'attribute').map((c) => c.value)}
                    onChange={(e) => {
                      const values = [...e.currentTarget.selectedOptions].map((o) => ({
                        userId: user.id,
                        accountId: 1,
                        name: 'attribute',
                        value: o.value,
                      }));
                      setFieldValue('claims', values);
                      handleChange(e);
                    }}
                  ></FormikDropdown>
                </Row>
                <Row>
                  <FormikText name="firstName" label="First Name:"></FormikText>
                  <FormikText name="middleName" label="Middle Name:"></FormikText>
                  <FormikText name="lastName" label="Last Name:"></FormikText>
                </Row>
                <FormikText name="phone" label="Phone:"></FormikText>
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
