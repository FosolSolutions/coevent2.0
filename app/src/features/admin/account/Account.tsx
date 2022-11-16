import {
  Button,
  ButtonVariant,
  castEnumToOptions,
  FormikCheckbox,
  FormikDropdown,
  FormikSelect,
  FormikText,
  FormikTextArea,
} from 'components';
import { Formik } from 'formik';
import { AccountType, IAccountModel, useApi } from 'hooks';
import React from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { IAccount } from '.';
import * as styled from './styled';
import { defaultAccount, toForm, toModel } from './utils';

/**
 * Account component properties.
 */
export interface IAccountProps {
  /**
   * Primary key to identify the account.
   */
  id?: number;
}

export const Account: React.FC<IAccountProps> = ({ id }) => {
  const params = useParams();
  id = parseInt(params.id ?? `${id}`);
  const api = useApi();
  const [account, setAccount] = React.useState<IAccount>(defaultAccount);
  const navigate = useNavigate();

  React.useEffect(() => {
    if (id) {
      api.accounts.get(id).then((data) => {
        setAccount(toForm(data));
      }); // TODO: Handle error.
    } else {
      setAccount(defaultAccount);
    }
  }, [api, id]);

  const handleDelete = async () => {
    await api.accounts.remove(toModel(account));
    await new Promise((r) => setTimeout(r, 30 * 1000));
    navigate('/admin/accounts');
  };

  return (
    <styled.Account>
      <div>
        <h1>{id === 0 && 'Add '}Account</h1>
        <div>
          {id !== 0 && (
            <Button variant={ButtonVariant.success} onClick={() => navigate('/admin/accounts/0')}>
              Add New
            </Button>
          )}
        </div>
      </div>
      <div>
        <Formik
          enableReinitialize
          initialValues={account}
          validate={(values) => {
            const errors = {} as any;
            if (!values.name) errors.name = 'Required';
            if (!values.ownerId) errors.ownerId = 'Required';
            return errors;
          }}
          onSubmit={async (values, { setSubmitting }) => {
            try {
              let data: IAccountModel;
              if (values.id === 0) {
                data = await api.accounts.add(toModel(values));
              } else {
                data = await api.accounts.update(toModel(values));
              }
              setAccount(toForm(data));
              navigate(`/admin/accounts/${data.id}`); // TODO: Find a way to update route without refreshing page.
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
                <FormikText name="name" label="Name:" value={values.name} required></FormikText>
                <FormikTextArea
                  name="description"
                  label="Description:"
                  value={values.description}
                ></FormikTextArea>
                <FormikCheckbox
                  name="isEnabled"
                  label="Enabled:"
                  checked={values.isEnabled}
                ></FormikCheckbox>
                <FormikDropdown
                  name="accountType"
                  label="Type:"
                  required
                  options={castEnumToOptions(AccountType)}
                ></FormikDropdown>
                <FormikSelect
                  name="ownerId"
                  label="Owner:"
                  options={[
                    { label: 'Admin', value: '1' },
                    { label: 'Fake 1', value: '2' },
                    { label: 'Fake 2', value: '3' },
                  ]}
                />
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
                  onClick={() => navigate('/admin/accounts')}
                  disabled={isSubmitting}
                >
                  Cancel
                </Button>
              </div>
            </form>
          )}
        </Formik>
      </div>
    </styled.Account>
  );
};
